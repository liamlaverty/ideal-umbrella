(async () => {
    console.log('ready')


    setupAllMaps();

})();

/** 
 * Renders all maps on a given page
 * 
 * Searches for any element with the class 'mb-map', and calls
 * the setupMap function
 * 
 */
function setupAllMaps () 
{
    // get all maps
    var mapCollection = document.getElementsByClassName('mb-map');

    // Get the access token, which should be available on the <body>
    // element, with the ID #body's data-mbkey property
    mapboxgl.accessToken = document.getElementById('body').dataset.mbkey;

    Array.prototype.forEach.call(mapCollection, function (mapEl) {
        setupMap(mapEl);
    })
}

/** 
 * Renders a single map by element 
 * 
 * Gets the following data attributes:
 * - data-map-lat
 * - data-map-lon
 * - data-map-zoom
 * 
 * throws an exception if it's passed a non Element type
 */
function setupMap(map) {
    if (!isElement(map)) {
        throw Error('Can only setup a map on a Element')
    }
    const thismap = new mapboxgl.Map({
        container: map.id,
        style: 'mapbox://styles/mapbox/streets-v12', // style URL
        center: [map.dataset.mapLon, map.dataset.mapLat], // starting position [lng, lat]
        zoom: map.dataset.mapZoom // starting zoom
    });
}

/** 
 * Verifies an object is a Element or HTMLDocument
 */
function isElement(element) {
    return element instanceof Element || element instanceof HTMLDocument;
}