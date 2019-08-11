// Create map at location
var mymap = L.map('TweetMap').setView([40.741461014133556, -73.99343490600587], 13);

// Populate map with provider
L.tileLayer('https://api.tiles.mapbox.com/v4/mapbox.streets/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiYWxvbmltdW4iLCJhIjoiY2p6NDh4YnQyMGF0MjNwbnljaGZtaXk4NCJ9.XkWuo7aZolmsqBcrC-1afA', {
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoiYWxvbmltdW4iLCJhIjoiY2p6NDh4YnQyMGF0MjNwbnljaGZtaXk4NCJ9.XkWuo7aZolmsqBcrC-1afA'
}).addTo(mymap);


// Initialize location to remove later
var circle = new L.circleMarker();

let allowClick = true;
// Add Map event to Create Circle in location
mymap.on('click', function (e) {
    if (!allowClick) return;

    allowClick = false;

    lastClick = new Date().getTime();
    // Remove previous
    mymap.removeLayer(circle);

    // Get location for circle
    var CircleLocation = e.latlng;

    circle = L.circle(CircleLocation, {
        color: 'lightblue',
        fillColor: 'lightblue',
        fillOpacity: 0.5,
        radius: 500
    }).addTo(mymap);

    var latLngObj = circle.getLatLng();

    $.get("/Home/GetTweetsInLocationRadius", { lat: latLngObj.lat, lng: latLngObj.lng, rad: circle.getRadius() }, function (data) {
        populateTweetsIntoTable(JSON.parse(data));
    });  
});

const populateTweetsIntoTable = (tweets) => {
    let tableBody = document.getElementById("tableBody");
    tableBody.innerHTML = '';

    for (const tweet of tweets) {
        let tweetText = tweet.full_text;
        let tweetUsername = tweet.user.name;

        let tweetElement = document.createElement("tr");
        let tweetUserElement = document.createElement("td");
        let tweetTextElement = document.createElement("td");

        tweetUserElement.innerText = tweetUsername;
        tweetTextElement.innerText = tweetText;

        tweetElement.appendChild(tweetUserElement);
        tweetElement.appendChild(tweetTextElement);

        tableBody.appendChild(tweetElement);

    }

    allowClick = true;
}