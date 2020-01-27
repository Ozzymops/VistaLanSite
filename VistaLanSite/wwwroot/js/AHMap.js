// OpenStreetMap API
// Position
var ahLat = 50.8906913;
var ahLon = 5.9842102;

// Construct AH map
ahMap = L.map('ahMap').setView([ahLat, ahLon], 17);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors',
    maxZoom: 17,
}).addTo(ahMap);

// Markers
ahMarker = L.marker([ahLat, ahLon]).addTo(ahMap);
ahMarker.bindPopup("<b>Albert Heijn</b><br />Schandelerboord 25<br />Heerlen").openPopup();