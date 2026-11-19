FROM mcr.microsoft.com/mssql/server:2022-latest
COPY --chown=mssql:root workers-init-db.sql /scripts/workers-init-db.sql
USER mssql

ENTRYPOINT ["/bin/bash", "-c", "exec /opt/mssql-tools18/bin/sqlcmd -S \"$SQLCMDSERVER\" -U \"$SQLCMDUSER\" -P \"$SQLCMDPASSWORD\" -C -b -I -v API_DB_PASSWORD=\"$API_DB_PASSWORD\" -i /scripts/workers-init-db.sql"]