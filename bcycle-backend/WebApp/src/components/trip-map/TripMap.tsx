import React, { useEffect, useRef } from 'react';
import { GoogleMap, Polyline, withGoogleMap, withScriptjs } from 'react-google-maps';

type LatLngLiteral = google.maps.LatLngLiteral;

interface TripMapProps {
  path: LatLngLiteral[];
}

const TripMap = withScriptjs(
  withGoogleMap((props: TripMapProps) => {
    const mapRef = useRef<GoogleMap>(null);

    useEffect(() => {
      if (mapRef.current) {
        const pathBounds = getBounds(props.path);
        mapRef.current.fitBounds(pathBounds);
      }
    });

    return (
      <GoogleMap ref={mapRef} options={{ disableDefaultUI: true }} clickableIcons={false}>
        <Polyline
          path={props.path}
          options={{
            strokeColor: '#0073ff',
            strokeOpacity: 1.0,
            strokeWeight: 2,
          }}
        />
      </GoogleMap>
    );
  })
);

export default TripMap;

const getBounds = (path: LatLngLiteral[]) => {
  const bounds = new google.maps.LatLngBounds();
  path.forEach((point) => {
    bounds.extend(point);
  });
  return bounds;
};
