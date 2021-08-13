FROM mcr.microsoft.com/mssql/server:2019-latest

RUN mkdir -p /opt/mssql-scripts

COPY src/Scripts/SeedData.sql /opt/mssql-scripts

ENV MSSQL_SA_PASSWORD=Numsey!2021
ENV ACCEPT_EULA=Y

RUN ( /opt/mssql/bin/sqlservr --accept-eula & ) | grep -q "Service Broker manager has started" \
    && sleep 10s \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P 'Numsey!2021' -i /opt/mssql-scripts/SeedData.sql \
    && pkill sqlservr