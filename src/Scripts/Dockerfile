FROM mcr.microsoft.com/mssql/server:2017-latest

RUN mkdir -p /opt/mssql-scripts

COPY src/Scripts/SeedData.sql /opt/mssql-scripts

ENV MSSQL_SA_PASSWORD=P@ssw0rd
ENV ACCEPT_EULA=Y

RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && sleep 10s \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'P@ssw0rd' -i /opt/mssql-scripts/SeedData.sql \
    && pkill sqlservr