﻿// Create map at location
let mymap = L.map('TweetMap').setView([40.741461014133556, -73.99343490600587], 13);

// Populate map with provider
L.tileLayer('https://api.tiles.mapbox.com/v4/mapbox.streets/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiYWxvbmltdW4iLCJhIjoiY2p6NDh4YnQyMGF0MjNwbnljaGZtaXk4NCJ9.XkWuo7aZolmsqBcrC-1afA', {
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoiYWxvbmltdW4iLCJhIjoiY2p6NDh4YnQyMGF0MjNwbnljaGZtaXk4NCJ9.XkWuo7aZolmsqBcrC-1afA'
}).addTo(mymap);


// Initialize location to remove later
let circle = new L.circleMarker();

let allowClick = true;
// Add Map event to Create Circle in location
mymap.on('click', function (e) {
    if (!allowClick) return;

    allowClick = false;

    lastClick = new Date().getTime();
    // Remove previous
    mymap.removeLayer(circle);

    // Get location for circle
    let CircleLocation = e.latlng;

    // Add Circle
    circle = L.circle(CircleLocation, {
        color: 'lightblue',
        fillColor: 'lightblue',
        fillOpacity: 0.5,
        radius: 500
    }).addTo(mymap);

    // Get Tweets within give location
    $.get("/Home/GetTweetsInLocationRadius", { lat: CircleLocation.lat, lng: CircleLocation.lng, rad: circle.getRadius() }, function (data) {
        populateTweetsIntoTable(JSON.parse(data));
    });  
});

// Populate server
const populateTweetsIntoTable = (tweets) => {

    // Get Table element
    let tableBody = document.getElementById("tableBody");

    // Initialize inner content
    tableBody.innerHTML = '';

    // Run through tweets and insert into table
    for (const tweet of tweets) {

        // Get Texts
        let tweetText = tweet.full_text;
        let tweetUsername = tweet.user.name;

        // create elements
        let tweetElement = document.createElement("tr");
        let tweetUserElement = document.createElement("td");
        let tweetTextElement = document.createElement("td");

        // Populate elements
        tweetUserElement.innerText = tweetUsername;
        tweetTextElement.innerText = tweetText;

        // Append to row
        tweetElement.appendChild(tweetUserElement);
        tweetElement.appendChild(tweetTextElement);

        //append to table
        tableBody.appendChild(tweetElement);
    }

    allowClick = true;
}