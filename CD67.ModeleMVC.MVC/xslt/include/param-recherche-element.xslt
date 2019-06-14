<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl cd67" xmlns:cd67 ="http://my.functions" >
  <xsl:include  href="include-recherche-pagination.xslt"/>
<!-- gestion de l'affichage des éléments en fonction du mode -->
<xsl:template name="affichageelement">
  <xsl:call-template name="paginateur"/>
<table class="list-result table">
    <!--<tr class="tr-p">
      <th>Thématique</th>
      <th>Solution/Problème</th>
    </tr>-->
		<!-- ajouter les différents cas de votre projet-->
    <xsl:choose>
  	<xsl:when test="$mode='modele-mvc'">
      <xsl:apply-templates select="response/result/doc" mode="modele-mvc" />
    </xsl:when>
    <xsl:otherwise>
      <xsl:apply-templates select="response/result/doc" mode="default" />
    </xsl:otherwise>
  </xsl:choose>
</table>
  <xsl:call-template name="paginateur"/>
</xsl:template>
<!-- fin de la gestion de l'affichage des éléments en fonction du mode -->
  
  <!-- affichage modele-mvc-->
  <xsl:template match="doc" mode="modele-mvc">
    <xsl:param name="id" select="str[@name='id']"></xsl:param>
    
    <tr>
      <xsl:if test="position() mod 2 = 0">
        <xsl:attribute name="class">tr-p</xsl:attribute>
      </xsl:if>
      <td class="wp-20">

        <b>
          <a href="/Viking/Details/{$id}" target="_blank">
            <xsl:value-of select="str[@name='nom']"/>
          </a>
        </b>
        <br/>
        <i>
          Description: <xsl:value-of select="str[@name='description']"/>
        </i>
        
          
      </td>
    </tr>
  </xsl:template>
 
</xsl:stylesheet>
