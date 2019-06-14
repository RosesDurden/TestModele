<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl cd67" xmlns:cd67 ="http://my.functions" >
  
    <xsl:template match="int" mode="facette">
    <xsl:if test=".>0">

      <xsl:variable name="fq"><xsl:value-of select="../@name" />:"<xsl:value-of select="@name"/>"</xsl:variable>
      
      <xsl:variable name="color">
      <xsl:choose>
        <xsl:when test="contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/.,$fq)">color1</xsl:when>
        <xsl:otherwise>facet-color</xsl:otherwise>
      </xsl:choose>
      </xsl:variable>

      <li>
        <a class="a-facet {$color}">

          <xsl:attribute name="href">
            ?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;rows=<xsl:value-of select="$recordsPerPage" />&amp;start=0<xsl:apply-templates select="/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']" mode="fq">
              <xsl:with-param name="facette" select="$fq"></xsl:with-param>
            </xsl:apply-templates>
            <xsl:if test="contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/., $fq)=false">&amp;fq=<xsl:value-of select="../@name" />:"<xsl:value-of select="@name"/>"</xsl:if>
          </xsl:attribute>
          
          <xsl:if test="contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/.,$fq)">
            <span class="sp140 {$color}" aria-hidden="true">&#9745;</span>
          </xsl:if>
          <xsl:if test="not(contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/.,$fq))">
            <span class="sp140" aria-hidden="true">&#9744;</span>
          </xsl:if>
		<xsl:text> </xsl:text>	

          <xsl:choose>
            <xsl:when test="string-length(substring-after(@name,'/'))!=0">
              <xsl:value-of select="substring-after(@name,'/')"/>
            </xsl:when>
            
            <!--Noms de résultats personnalisés-->
            <xsl:when test="@name='Marches'">Marchés</xsl:when>

            <xsl:otherwise>
              <xsl:value-of select="@name"/>
            </xsl:otherwise>
          </xsl:choose> (<xsl:value-of select="text()" />)
        </a>

      </li>
    </xsl:if>
  </xsl:template>
  
  <xsl:template match="int" mode="facette-range">

    <!--<xsl:if test=".>0">-->

      <xsl:variable name="fq"><xsl:value-of select="../../@name" />:[<xsl:value-of select="@name"/> TO <xsl:value-of select="number(@name) + (number(../../int[@name='gap'])-1)"/>]</xsl:variable>
    <!--<span class="no"> <xsl:value-of select="$fq"/>
    </span>-->
      <xsl:variable name="color">
      <xsl:choose>
        <xsl:when test="contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/.,$fq)">color1</xsl:when>
        <xsl:otherwise>facet-color</xsl:otherwise>
      </xsl:choose>
      </xsl:variable>

      <li>
        <a class="a-facet a-facet-range {$color}">

          <xsl:attribute name="href">
            ?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;rows=<xsl:value-of select="$recordsPerPage" />&amp;start=0<xsl:apply-templates select="/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']" mode="fq">
              <xsl:with-param name="facette" select="$fq"></xsl:with-param>
            </xsl:apply-templates>
            <xsl:if test="contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/., $fq)=false">&amp;fq=<xsl:value-of select="../../@name" />:[<xsl:value-of select="@name"/> TO <xsl:value-of select="number(@name) + (number(../../int[@name='gap'])-1)"/>]</xsl:if>
          </xsl:attribute>
          
          <xsl:if test="contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/.,$fq)">
            <span class="sp140 {$color}" aria-hidden="true">&#9745;</span>
          </xsl:if>
          <xsl:if test="not(contains(/response/lst[@name='responseHeader']/lst[@name='params']/*[@name='fq']/.,$fq))">
            <span class="sp140" aria-hidden="true">&#9744;</span>
          </xsl:if>
		<xsl:text> </xsl:text>	

          <xsl:choose>
            <xsl:when test="string-length(substring-after(@name,'/'))!=0">
              <xsl:value-of select="substring-after(@name,'/')"/>
            </xsl:when>
            
            <!--Noms de résultats personnalisés-->
            <xsl:when test="@name='Marches'">Marchés</xsl:when>

            <xsl:otherwise>
              <xsl:value-of select="@name"/> à <xsl:value-of select="number(@name) + (number(../../int[@name='gap'])-1)"/>
            </xsl:otherwise>
          </xsl:choose> (<xsl:value-of select="text()" />)
        </a>

      </li>
    <!--</xsl:if>-->
  </xsl:template>

  <!-- suppression des facette -->
  <xsl:template match="str" mode="delfacet">

    <a class="delfacet">
      <xsl:variable name="fq"><xsl:value-of select="."/></xsl:variable>

      <xsl:attribute name="href">
        ?rows=<xsl:value-of select="$recordsPerPage" />&amp;start=0<xsl:for-each select="//descendant-or-self::str[@name='fq']|//descendant-or-self::arr[@name='fq']/str"><xsl:if test="contains(., $fq)=false">&amp;fq=<xsl:value-of select="."/></xsl:if>
        </xsl:for-each>
      </xsl:attribute>

      <xsl:text>X </xsl:text>
      <xsl:value-of select="."/>
    </a>

  </xsl:template>
    <xsl:template match="str" mode="fq">
    <xsl:param name="facette" />
    <xsl:if test="not($facette=text())">&amp;fq=<xsl:value-of select="text()"/></xsl:if>
  </xsl:template>

  <xsl:template match="arr" mode="fq">
    <xsl:param name="facette" />
    <xsl:apply-templates select="str" mode="fq">
      <xsl:with-param name="facette" select="$facette"></xsl:with-param>
    </xsl:apply-templates>
  </xsl:template>

  <xsl:template match="str" mode="facet">&amp;fq=<xsl:value-of select="."/></xsl:template>

 

</xsl:stylesheet>