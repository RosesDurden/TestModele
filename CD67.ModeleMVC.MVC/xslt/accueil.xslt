<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>
  <xsl:preserve-space elements="" />
  
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

  
  <xsl:template match ="/">
    <br/>
    <h2>Les derniers ajouts dans la base de connaissances</h2>
    <table class="table">
      <thead>
        <tr class="tr-p">
          <th>Thématique</th>
          <th>Solution/Problème</th>
        </tr>
      </thead>
      <tbody>
        <xsl:apply-templates select ="//doc"></xsl:apply-templates>
      </tbody>
    </table>
  </xsl:template>
      <xsl:template match="doc">
        <tr>
          <xsl:if test="position() mod 2 = 0">
            <xsl:attribute name="class">tr-p</xsl:attribute>
          </xsl:if>
          <!-- class="element-result"-->
          <!--<a href="/Applications/DetailApplication?id={str[@name='codeappli']}">-->
          <td class="wp-20">
            <!--<xsl:value-of select ="str[@name='nomappli']"/></a>-->

            <xsl:value-of select="str[@name='idappli']" /> - <xsl:value-of select ="str[@name='nomappli']"/>


          </td>
          <td class="wp-100">
            <!--<a href="/Applications/DetailProbleme?id={str[@name='id']}" title="voir la fiche du probleme">-->
            <b>
              <xsl:value-of select ="str[@name='probleme']"/>
            </b>
            <br></br>
            <div class="btn toggle">Afficher</div>
            <div class="solution">
              
              <xsl:call-template name="nl2br">
                <xsl:with-param name="contents" select="str[@name='solution']" />
              </xsl:call-template>
              <xsl:if test ="count(arr[@name='piecejointe']/str) &gt; 0">
                <p>
                  <b>
                    <xsl:choose>
                      <xsl:when test ="count(arr[@name='piecejointe']/str) = 1">
                        Voir aussi dans la piece jointe suivante
                      </xsl:when>
                      <xsl:otherwise>
                        Voir aussi dans les pieces jointes suivantes
                      </xsl:otherwise>
                    </xsl:choose>
                  </b>
                </p>
                <ul>
                  <xsl:for-each select ="arr[@name='piecejointe']/str">
                    <li>
                      <a href="{substring-before(.,'||||')}" target="_blank">
                        <xsl:value-of select ="substring-after(.,'||||')"/>
                      </a>
                    </li>
                  </xsl:for-each>

                </ul>
              </xsl:if>
            </div>
            <!--</a>-->
          </td>
        </tr>

      </xsl:template>
</xsl:stylesheet>
