// OpenStreetMap API
// Position
var vistaLat = 50.8800119;
var vistaLon = 5.9563456;

// Construct VISTA map
vistaMap = L.map('vistaMap').setView([vistaLat, vistaLon], 16);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors',
    maxZoom: 16,
}).addTo(vistaMap);

// Markers
vistaMarker = L.marker([vistaLat, vistaLon]).addTo(vistaMap);
vistaMarker.bindPopup("<b>VISTA College</b><br />Valkenburgerweg 148<br />Heerlen").openPopup();