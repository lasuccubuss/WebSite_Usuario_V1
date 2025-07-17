<?xml version="1.0" encoding="utf-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/Usuarios">
		<html>
			<body>
				<h2>Relatório de Usuários Ativos</h2>
				<table border="1">
					<tr bgcolor="#9acd32">
						<th>Nome</th>
						<th>Email</th>
					</tr>
					<!-- Aqui o for-each percorre cada usuário -->
					<xsl:for-each select="Usuario">
						<tr>
							<td>
								<xsl:value-of select="Nome"/>
							</td>
							<td>
								<xsl:value-of select="Email"/>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
