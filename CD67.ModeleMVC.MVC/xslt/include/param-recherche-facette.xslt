<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl cd67" xmlns:cd67 ="http://my.functions" >
	<!-- gestion du nom des titre de facette -->
<xsl:template match="lst" mode="facette">
   
        <h4 class="titre-facet">
          <xsl:choose>
            
			<!-- ajouter les différents cas -->
            <xsl:when test="@name='type_libelle'">Type</xsl:when>
            <xsl:when test="@name='casque_cornu'">Casque cornu</xsl:when>
           
            <xsl:when test="@name='annee_creation'">Année de création</xsl:when>
			<!-- ne pas toucher en dessous -->
            <xsl:otherwise>
              <xsl:value-of select="@name"/>
            </xsl:otherwise>
          </xsl:choose>
        </h4>
        <ul class="ul-facet">
          <xsl:apply-templates select="int" mode="facette" />
        </ul>
      
  </xsl:template>

  <xsl:template match="lst" mode="facette-range">

    <h4 class="titre-facet">
      <xsl:choose>

        <!-- ajouter les différents cas -->
        <xsl:when test="../@name='nb_victoires'">Nombre de victoires</xsl:when>
     
        <!-- ne pas toucher en dessous -->
        <xsl:otherwise>
          <xsl:value-of select="../@name"/>
        </xsl:otherwise>
      </xsl:choose>
    </h4>
    <ul class="ul-facet">
      <xsl:apply-templates select="int" mode="facette-range" />
    </ul>

  </xsl:template>
</xsl:stylesheet>