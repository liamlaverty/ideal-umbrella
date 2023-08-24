# (Unofficial) Climate Trace Data Downloader 

Downloads & unzips all inventory datasets from the ClimateTrace repository: https://climatetrace.org/

Companion app to https://github.com/liamlaverty/climate-trace-data-importer


## Copyright Note 

This repository uses the MIT License. The emissions inventory downloaded from ClimateTrace have different licenses, see https://climatetrace.org/faq for details


## What Does It Do?

* Downloads national ClimateTrace inventories for each country, for the categories:
  * Forest Sector
  * Non-Forest Sector
* Downloads international CliamteTrace inventories for the categories:
  * Agriculture
  * Buildings
  * Forestry and land use
  * Fossil Fuel Operations
  * Manufacturing
  * Power
  * Transportation
  * Waste
* Unzips the downloaded data

Once the download & unzipping are complete, a directory containing the following structure will be created, containing CSVs of inventories
```
~/
    -> /forest_sectors_data/
        -> /ABW/
        -> /AFG/
        -> /AGO/
    -> /non_forest_sectors_data/
        -> /ABW/
        -> /AFG/
        -> /AGO/
    -> /country_sector_data/
        -> /sector_packages/
            -> /agriculture/
            -> /buildings/
            -> /plus /
```

## Usage
* in the project's main directory, run `ClimateTraceDownloader.DownloadData()`

### Optional Params

These can be found in the `../IU.ClimateTrace.Downloader.ConsoleApp` project's `appsettings.json` file. A companion `appsettings-schema.json` file offers a description of these settings.


* `DownloadDataPath`               
  * The output path. 


* `EnableDownloadCountryData`           
  * Boolean value, defaults to `true`. 
  * When set to false, skips downloading of aggregated international data (the `sector_packages` dataset)

* `EnableDownloadForestryData`      
  * Boolean value, defaults to `true`
  * When set to false, skips downloading of country-level forest sector data (the `forest_sectors` datasets)

* `EnableDownloadNonForestryData`   
  * Boolean value, defaults to `true`
  * When set to false, skips downloading of country-level non-forest sector data (the `non_forest_sectors` datasets)

* `EnableUnzipAfterDownload`                
  * Boolean value, defaults to `true`
  * When set to false, skips unzipping of the downloaded files
  * Usage: `python .\climate-trace.py --skipUnzipFiles`

* `SpecifyCountries`                
  * array of strings, defaults to empty
  * When set,  downloads specified countries
  * Accepts an array of ISO-3166-1 alpha-3 countries (see https://en.wikipedia.org/wiki/ISO_3166-1_alpha-3)
    * raises an exception if any invalid country is given as a param

* `CountryDataDownloadFileSets`
  * Array of strings, defaults to the climateTrace datasets. Disable the downloading of datasets by excluding from this list.

## Requirements

* At least 2TB hard drive space for the entire download, plus its unzipped files
