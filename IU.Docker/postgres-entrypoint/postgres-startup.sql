create table if not exists country_emissions
(
    id                        serial            ,
    iso3_country              char(3)           not null,
    start_time                TIMESTAMP         ,
    end_time                  TIMESTAMP         ,
    original_inventory_sector varchar(80)       not null,
    gas                       char(11)          not null,
    emissions_quantity        bigint default 0  not null,
    emissions_quantity_units  varchar(32)       not null,
    temporal_granularity      varchar(32)       not null,
    origin_source             varchar(32)       not null,
    source_created_date       TIMESTAMP         ,
    source_modified_date      TIMESTAMP         ,
    created_date              TIMESTAMP         ,
    modified_date             TIMESTAMP         ,

    PRIMARY KEY (iso3_country, start_time, gas, temporal_granularity, original_inventory_sector)
);

CREATE TABLE IF NOT EXISTS asset_emissions_test 
(
    asset_id                        INTEGER             NOT NULL,
    iso3_country                    VARCHAR(3)          NOT NULL,
    original_inventory_sector       VARCHAR(40)         NOT NULL,
    start_time                      TIMESTAMP           NOT NULL,
    end_time                        TIMESTAMP           NOT NULL,
    temporal_granularity            VARCHAR(30)         NOT NULL,
    gas                             VARCHAR(11)         NOT NULL,
    emissions_quantity              BIGINT              ,
    emissions_factor                NUMERIC             ,
    emissions_factor_units          VARCHAR(50)         NOT NULL,
    capacity                        NUMERIC             ,
    capacity_units                  VARCHAR(40)         ,
    capacity_factor                 NUMERIC             ,
    activity                        NUMERIC             ,
    activity_units                  VARCHAR(40)         ,
    origin_source                   varchar(32)         not null,
    source_created_date             TIMESTAMP           ,
    source_modified_date            TIMESTAMP           ,
    created_date                    TIMESTAMP           NOT NULL,
    modified_date                   TIMESTAMP           ,
    asset_name                      VARCHAR(256)        NOT NULL,
    asset_type                      VARCHAR(100)        NOT NULL,
    st_astext                       GEOMETRY            NOT NULL, 

    PRIMARY KEY (iso3_country, asset_id, start_time, gas, temporal_granularity)
)

CREATE TABLE IF NOT EXISTS asset_owners 
(

)