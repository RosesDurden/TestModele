<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl cd67" xmlns:cd67 ="http://my.functions" >
  <xsl:output method="xml" indent="no" omit-xml-declaration="yes" />
  <xsl:strip-space elements ="a"/>
  <xsl:preserve-space elements="" />
  
  <!-- inclusion xslt -->
  <!--<xsl:include  href="include/include-recherche-pagination.xslt"/>-->
  <xsl:include href="include/include-recherche-tools.xslt"/>
  <xsl:include href="include/include-recherche-facette.xslt"/>
  <xsl:include href="include/param-recherche-facette.xslt"/>
  <xsl:include href="include/param-recherche-element.xslt"/>

  <!-- inclusion de la configuration solr -->
  <xsl:param name="configFile" />
  <xsl:variable name="solrconfig" select="document($configFile)"/>

  <!-- parametre du nombre de page afficher dans le paginateur -->
  <xsl:variable name="cantPages" select="3" />
  <xsl:variable name="numberOfRecords" select="response/result/@numFound"/>
  <xsl:variable name="recordsPerPage" select="/response/lst[@name='responseHeader']/lst[@name='params']/str[@name='rows']" />
  <xsl:variable name="pageNumber" select="(response/result/@start div $recordsPerPage) + 1"/>
  <xsl:variable name="start" select="response/result/@start"/>
  <xsl:variable name="endPage" select="ceiling($numberOfRecords div $recordsPerPage)" />
  <xsl:variable name="query" select="/response/lst[@name='responseHeader']/lst[@name='params']/str[@name='q']" />
  
  <xsl:param name="recherche" />
  <xsl:param name="paramrecherche" />
  <xsl:param name="extend" />
  
  <!-- passage des param pour le multi langue-->
  <xsl:param name="test" />
  <xsl:param name="urlini" />
  
  <!-- parametre pour choix de l'affichage -->
  <xsl:param name="mode" />
  
  <!-- debut de la generation de la page -->
  <xsl:template match="/">

   <div class="flex">
		<div class="result col-md-10">
        <!-- si pas de resultat -->
		<xsl:if test="not(response/result/doc)">
          <xsl:call-template name="noresult"/>
        </xsl:if>

        <xsl:if test="$numberOfRecords>0">
          <!--<div class="nombre_resultat float">
            <p>
			      <xsl:value-of select="$numberOfRecords" />
            <xsl:text> </xsl:text>
            <xsl:if test="$numberOfRecords>1">
			        résultats pour votre recherche
            </xsl:if>
            <xsl:if test="$numberOfRecords=1">
			        résultat pour votre recherche
            </xsl:if>
            </p>
          </div>-->
          
		      <!-- affichage de la pagination haut 
		      <xsl:call-template name="paginateur"/>-->

              <!-- affichage de la liste de résultat -->
		      <xsl:call-template name="affichageelement"/>
        </xsl:if>
      <!--<div class="panel top">
       
        <a href="#top" class="color2"> &#9652; Haut</a>
      </div>-->
		</div>
		
		<!-- affichage des facette-->
		<div class="panel facet col-md-3">

      <!-- si pas de resultat -->
      <xsl:if test="not(response/result/doc)">
        <xsl:call-template name="noresult"/>
      </xsl:if>

      <xsl:if test="$numberOfRecords>0">
        <div class="nombre_resultat float">
          <br></br>
          <p>
            <xsl:value-of select="$numberOfRecords" />
            <xsl:text> </xsl:text>
            <xsl:if test="$numberOfRecords>1">
              résultats pour votre recherche
            </xsl:if>
            <xsl:if test="$numberOfRecords=1">
              résultat pour votre recherche
            </xsl:if>
            : <xsl:value-of select="$recherche"/>
          </p>
        </div>

       
      </xsl:if>

      <h2>Affiner par :</h2>
      <xsl:apply-templates select="//response/lst[@name='facet_counts']/lst[@name='facet_fields']/lst" mode="facette" />

      <!-- affichage des facettes de type range-->
      <xsl:apply-templates select="//response/lst[@name='facet_counts']/lst[@name='facet_ranges']/lst/lst" mode="facette-range" />

      <!-- ne pas supprimer, necessaire pour bloquer la largeur, recherche une solution plus elegante -->
			<!--<img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAMAAAFfzAQtAAAABlBMVEUAAAD///+l2Z/dAAAAAnRSTlP/AOW3MEoAAAAKSURBVHjaY2AEAAADAALmfadnAAAAAElFTkSuQmCC" alt="" width="300" height="1"/>-->

      
    </div>
     
	</div>
  </xsl:template>
</xsl:stylesheet>
