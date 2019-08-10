// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
var mymap = L.map('TweetMap').setView([51.505, -0.09], 13);


L.tileLayer('https://api.tiles.mapbox.com/v4/mapbox.streets/{z}/{x}/{y}.png?access_token=pk.eyJ1IjoiYWxvbmltdW4iLCJhIjoiY2p6NDh4YnQyMGF0MjNwbnljaGZtaXk4NCJ9.XkWuo7aZolmsqBcrC-1afA', {
    maxZoom: 18,
    id: 'mapbox.streets',
    accessToken: 'pk.eyJ1IjoiYWxvbmltdW4iLCJhIjoiY2p6NDh4YnQyMGF0MjNwbnljaGZtaXk4NCJ9.XkWuo7aZolmsqBcrC-1afA'
}).addTo(mymap);

var marker = L.marker([51.5, -0.09]).addTo(mymap);