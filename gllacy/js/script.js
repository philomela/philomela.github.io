function initMap() {
        var uluru = {lat: 55.788913, lng: 37.597336};
        var map = new google.maps.Map(document.getElementById('map'), {
          zoom: 17,
          center: uluru
        });
        var marker = new google.maps.Marker({
          position: uluru,
          map: map
        });
      }
