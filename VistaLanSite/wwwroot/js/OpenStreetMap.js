// OpenStreetMap API
// Position
var lat = 50.8800119;
var lon = 5.9563456;

// Construct map
map = L.map('openStreetMap').setView([lat, lon], 16);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors',
    maxZoom: 16,
}).addTo(map);

// Markers
marker = L.marker([lat, lon]).addTo(map);
marker.bindPopup("<b>VISTA College</b><br />Valkenburgerweg 148<br />Heerlen").openPopup();