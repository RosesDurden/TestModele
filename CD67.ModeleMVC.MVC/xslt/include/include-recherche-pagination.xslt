<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl cd67" xmlns:cd67 ="http://my.functions" >
 <xsl:strip-space elements ="a"/>

	<xsl:template match="result" mode="pagination">

    <xsl:if test="$recordsPerPage &lt; $numberOfRecords">
      <xsl:if test="$pageNumber &gt; 1">

        <xsl:choose>
          <xsl:when test ="$query='*:*'">
            <a class="numeroPagination" href="?start={($pageNumber - 2) * $recordsPerPage}">
				&#9665; précédent"
            </a> |
          </xsl:when>
          <xsl:otherwise>
            <a class="numeroPagination" href="?term={$query}&amp;start={($pageNumber - 2) * $recordsPerPage}">
				&#9665; précédent
            </a> |
          </xsl:otherwise>
        </xsl:choose>


      </xsl:if>
      <xsl:call-template name="numerosPaginas">
        <xsl:with-param name="current" select="$pageNumber"/>
        <xsl:with-param name="max">
          <xsl:choose>
            <xsl:when test="(($pageNumber + $cantPages) &gt; $endPage) or ($endPage &lt;= 9)">
              <xsl:value-of select="$endPage" />
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="($pageNumber + $cantPages)" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:with-param>
        <xsl:with-param name="number">
          <xsl:choose>
            <xsl:when test="(($pageNumber - $cantPages) &lt; 1) or ($endPage &lt;= 9)">
              <xsl:value-of select="1" />
            </xsl:when>
            <xsl:otherwise>
              <xsl:value-of select="($pageNumber - $cantPages)" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:with-param>

      </xsl:call-template>
      <xsl:if test="(($pageNumber ) * $recordsPerPage) &lt; ($numberOfRecords)">

        <xsl:choose>
          <xsl:when test ="$query='*:*'">
            | <a href="?start={($pageNumber) * $recordsPerPage}">
              suivant  &#9655;
            </a>
          </xsl:when>
          <xsl:otherwise>
            | <a href="?term={$query}&amp;start={($pageNumber) * $recordsPerPage}">
              suivant &#9655;
            </a>
          </xsl:otherwise>
        </xsl:choose>

      </xsl:if>

      (Nombre de resultats <xsl:value-of select="$numberOfRecords" />)

    </xsl:if>

  </xsl:template>
  
  <xsl:template name="numerosPaginas">
    <xsl:param name="current"/>
    <xsl:param name="number"/>
    <xsl:param name="max"/>

    <xsl:choose>
      <xsl:when test="$number = $current">
        <!-- la page courante n'est pas un lien -->
        <span class="currentPagination">
          <xsl:value-of select="$number"/>
        </span>
      </xsl:when>
      <xsl:otherwise>

        <xsl:choose>
          <xsl:when test ="$query='*:*'">
            <!--<a class="numeroPagination" href="?start={($number - 1) * $recordsPerPage}">-->
            <a class="numeroPagination">
              <xsl:attribute name="href">
                ?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;start=<xsl:value-of select="(($number - 1)  * $recordsPerPage)"/>&amp;rows=<xsl:value-of select="$recordsPerPage" /><xsl:apply-templates select="//descendant-or-self::str[@name='fq']|//descendant-or-self::arr[@name='fq']/str" mode="facet" />
              </xsl:attribute>
              <xsl:value-of select="$number"/>
            </a>
          </xsl:when>
          <xsl:otherwise>
            <!--<a class="numeroPagination" href="?q={$query}&amp;start={($number - 1) * $recordsPerPage}&amp;rows={$recordsPerPage}">
			  
                <xsl:value-of select="$number"/>
              </a>-->
            <a class="numeroPagination">
              <xsl:attribute name="href">
                ?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;start=<xsl:value-of select="(($number - 1)  * $recordsPerPage)"/>&amp;rows=<xsl:value-of select="$recordsPerPage" /><xsl:apply-templates select="//descendant-or-self::str[@name='fq']|//descendant-or-self::arr[@name='fq']/str" mode="facet" />
              </xsl:attribute>
              <xsl:value-of select="$number"/>
            </a>

          </xsl:otherwise>
        </xsl:choose>

      </xsl:otherwise>
    </xsl:choose>

    <!-- Appel recursif jusqu'a nb max de page -->
    <xsl:if test="$number &lt; $max">
      <xsl:call-template name="numerosPaginas">
        <xsl:with-param name="current" select="$current"/>
        <xsl:with-param name="number" select="$number+1"/>
        <xsl:with-param name="max" select="$max"/>
      </xsl:call-template>
    </xsl:if>
  </xsl:template>

  
  <xsl:template name="paginateur">

 

            <xsl:if test="$recordsPerPage &lt; $numberOfRecords">

              <div class="pagination">
                <span>
                  <xsl:if test="$pageNumber &gt; 1">                
                    <xsl:choose>
                      <xsl:when test ="$query='*:*'">
                        <a class="firstPagination" href="?start={($pageNumber - 2) * $recordsPerPage}">
                         &#9665; précédent
                        </a>
                      </xsl:when>
                      <xsl:otherwise>
                        <a class="firstPagination" >
                          <xsl:attribute name="href">?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;start=<xsl:value-of select="($pageNumber - 2) * $recordsPerPage"/>&amp;rows=<xsl:value-of select="$recordsPerPage" /><xsl:apply-templates select="//descendant-or-self::str[@name='fq']|//descendant-or-self::arr[@name='fq']/str" mode="facet" />
                          </xsl:attribute>                          
                          &#9665; précédent
                        </a>
                      </xsl:otherwise>
                    </xsl:choose>


                  </xsl:if>
                  <xsl:call-template name="numerosPaginas">
                    <xsl:with-param name="current" select="$pageNumber"/>
                    <xsl:with-param name="max">
                      <xsl:choose>
                        <xsl:when test="(($pageNumber + $cantPages) &gt; $endPage) or ($endPage &lt;= 9)">
                          <xsl:value-of select="$endPage" />
                        </xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of select="($pageNumber + $cantPages)" />
                        </xsl:otherwise>
                      </xsl:choose>
                    </xsl:with-param>
                    <xsl:with-param name="number">
                      <xsl:choose>
                        <xsl:when test="(($pageNumber - $cantPages) &lt; 1) or ($endPage &lt;= 9)">
                          <xsl:value-of select="1" />
                        </xsl:when>
                        <xsl:otherwise>
                          <xsl:value-of select="($pageNumber - $cantPages)" />
                        </xsl:otherwise>
                      </xsl:choose>
                    </xsl:with-param>

                  </xsl:call-template>
                  <xsl:if test="(($pageNumber ) * $recordsPerPage) &lt; ($numberOfRecords)">

                    <xsl:choose>
                      <xsl:when test ="$query='*:*'">                      
                        <a class="lastPagination">
                          <xsl:attribute name="href">                            
                            ?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;start=<xsl:value-of select="($pageNumber * $recordsPerPage)"/>&amp;rows=<xsl:value-of select="$recordsPerPage" /><xsl:apply-templates select="//descendant-or-self::str[@name='fq']|//descendant-or-self::arr[@name='fq']/str" mode="facet" />
                          </xsl:attribute>                          
                          suivant &#9655;
                        </a>
                      
                      </xsl:when>
                      <xsl:otherwise>                        
                        <a class="lastPagination">
                          <xsl:attribute name="href">
                            ?<xsl:value-of select="$paramrecherche" />=<xsl:value-of select="$recherche" /><xsl:value-of select="$extend" />&amp;start=<xsl:value-of select="($pageNumber * $recordsPerPage)"/>&amp;rows=<xsl:value-of select="$recordsPerPage" /><xsl:apply-templates select="//descendant-or-self::str[@name='fq']|//descendant-or-self::arr[@name='fq']/str" mode="facet" />
                          </xsl:attribute>
                          suivant &#9655;
                        </a>
                      </xsl:otherwise>
                    </xsl:choose>

                  </xsl:if>
                </span>
                
              </div>
            </xsl:if>

</xsl:template>

</xsl:stylesheet>