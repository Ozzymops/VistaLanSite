// OpenStreetMap API
// Positions
var olLat = 52.2626079;
var olLon = 4.6289755;
var maastrichtLat = 50.9303447;
var maastrichtLon = 5.7782772;
var eindhovenLat = 51.436325;
var eindhovenLon = 5.4251385;
var amsterdamLat = 52.3123798;
var amsterdamLon = 4.9380253;
var landgraafLat = 50.8920645;
var landgraafLon = 6.0256474;
var ijsselLat = 51.9130392;
var ijsselLon = 4.5392901;

// Construct OL map
olMap = L.map('olMap').setView([olLat, olLon], 6.75);
L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/">OpenStreetMap</a> contributors',
    maxZoom: 17,
}).addTo(olMap);

// Markers
maastrichtMarker = L.marker([maastrichtLat, maastrichtLon]).addTo(olMap);
eindhovenMarker = L.marker([eindhovenLat, eindhovenLon]).addTo(olMap);
amsterdamMarker = L.marker([amsterdamLat, amsterdamLon]).addTo(olMap);
ijsselMarker = L.marker([ijsselLat, ijsselLon]).addTo(olMap);
landgraafMarker = L.marker([landgraafLat, landgraafLon]).addTo(olMap);

maastrichtMarker.bindPopup("<b>Open Line</b><br />Amerikalaan 90<br />Maastricht Airport").openPopup();
eindhovenMarker.bindPopup("<b>Open Line</b><br />Hurksestraat 29-51<br />Eindhoven").openPopup();
amsterdamMarker.bindPopup("<b>Open Line</b><br />De Entree 143<br />Amsterdam").openPopup();
ijsselMarker.bindPopup("<b>Open Line</b><br />Rivium Westlaan 1<br />Capelle a/d IJssel").openPopup();
landgraafMarker.bindPopup("<b>Open Line</b><br />Minckelersstraat 2<br />Landgraaf").openPopup();