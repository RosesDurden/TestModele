<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl cd67" xmlns:cd67 ="http://my.functions" >
  <!-- fonctionnalité cd67 pour remplacement de chaine en c# -->
  <msxsl:script implements-prefix="cd67" language="c#">

    <![CDATA[
     
      
		public string ReplaceChaine( string inp, string rec , string rep){
    
			return inp.Replace(rec, rep);
    
		}

         ]]>

  </msxsl:script>

  
  <xsl:template match="lst"  mode="exportcustom">

    <div class="blocks block_rss abonner-rss">
      <i class="icon-"></i>
      <a href="/proxy/export.ashx?{$urlini}&amp;wt=xslt&amp;tr=xml2rss.xslt&amp;hl=false" target="rss">
        Rss 
      </a>
    </div>

  </xsl:template>
  
  
  <!-- export -->

  <xsl:template match="lst"  mode="export">

    <ul class="export">
      <li>
        <a href="/proxy/export.ashx?{$urlini}&amp;wt=csv">csv</a>
      </li>
      <li>
        <a href="/proxy/export.ashx?{$urlini}&amp;wt=xml&amp;hl=false&amp;omitHeader=true">xml</a>
      </li>
      <li>
        <a href="/proxy/export.ashx?{$urlini}&amp;wt=json&amp;hl=false&amp;omitHeader=true">json</a>
      </li>
      <li>
        <a href="/proxy/export.ashx?{$urlini}&amp;wt=xslt&amp;tr=xml2rss.xslt&amp;hl=false">rss</a>
      </li>
    </ul>

  </xsl:template>
  
<!-- affichage des resultats par défaut -->
  <xsl:template match="doc" mode="default">
    <li>
      <ul>
        <xsl:apply-templates select="int|att|arr|str" mode="doc"/>
      </ul>
    </li>
  </xsl:template>

  <!-- mise en forme des élémenrs de résultats  par défaut-->
  <xsl:template match="int|arr|str" mode="doc">
    <li>
      <strong>
        <xsl:value-of select="@name"/> :
      </strong>
      <xsl:value-of select="node()|descendant-or-self::node()"/>
      <br/>
    </li>
  </xsl:template>
  
  <!-- affichage pas de résultat -->
  <xsl:template name="noresult">
  
  <div class="nombre_resultat float">
            <p>
              Aucun résultat pour votre recherche : <xsl:value-of select="$query" />
            </p>
          </div>

  </xsl:template>
  <!-- fin  affichage pas de résultat -->

  
  <xsl:template match="@*">
  <xsl:attribute name="{name()}">
         <xsl:value-of select="normalize-space(.)" />
      </xsl:attribute>
  </xsl:template>
  
   <xsl:template name="nl2br">
    <xsl:param name="contents" />

    <xsl:choose>
      <xsl:when test="contains($contents, '&#10;')">
        <xsl:value-of select="substring-before($contents, '&#10;')" disable-output-escaping="yes" />
        <br />
        <xsl:call-template name="nl2br">
          <xsl:with-param name="contents" select="substring-after($contents, '&#10;')" />
        </xsl:call-template>
      </xsl:when>
      <xsl:otherwise>
        <xsl:value-of select="$contents" disable-output-escaping="yes" />
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>
  
</xsl:stylesheet>