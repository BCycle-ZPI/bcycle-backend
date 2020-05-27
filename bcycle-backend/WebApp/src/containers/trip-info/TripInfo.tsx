import { Box, Grid } from '@material-ui/core';
import React, { PropsWithChildren, useMemo } from 'react';
import Scrollbars from 'react-custom-scrollbars';
import './TripInfo.scss';
import TripPhotos from '../../components/trip-photos/TripPhotos';
import { RoutePoint } from '../../core/model';
import TripMap from '../../components/trip-map/TripMap';

interface TripInfoProps {
  photosUrls: string[];
  route: RoutePoint[];
}

const googleMapsUrl = `https://maps.googleapis.com/maps/api/js?v=3.exp&libraries=geometry,drawing,places&key=${process.env.REACT_APP_GOOGLE_API_KEY}`;

export default function TripInfo(props: PropsWithChildren<TripInfoProps>) {
  const tripPath = useMemo(() => transformPath(props.route), [props.route]);

  return (
    <Grid id="wrapper" container justify="space-between">
      <Grid id="map-wrapper" container item direction="column" xs>
        <Box boxShadow={2} height="100%">
          <TripMap
            path={tripPath}
            googleMapURL={googleMapsUrl}
            loadingElement={<div className="map-element" />}
            containerElement={<div className="map-element" />}
            mapElement={<div className="map-element" />}
          />
        </Box>
      </Grid>

      <Grid id="info-wrapper" container item direction="column" xs>
        <Scrollbars id="scroll" autoHide>
          {props.children}
          {props.photosUrls.length > 0 ? renderPhotos(props.photosUrls) : <></>}
        </Scrollbars>
      </Grid>
    </Grid>
  );
}

const transformPath = (route: RoutePoint[]) =>
  route.sort(ascendingComparator).map((point) => ({ lat: point.latitude, lng: point.longitude }));

const ascendingComparator = (p1: RoutePoint, p2: RoutePoint) => getTime(p1.timeReached) - getTime(p2.timeReached);

const getTime = (dateString: string): number => new Date(dateString).getTime();

const renderPhotos = (photosUrls: string[]) => (
  <Grid item>
    <TripPhotos photosUrls={photosUrls} />
  </Grid>
);
