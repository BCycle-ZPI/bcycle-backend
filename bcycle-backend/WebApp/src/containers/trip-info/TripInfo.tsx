import { Box, Grid } from "@material-ui/core";
import React, { PropsWithChildren } from "react";
import Scrollbars from "react-custom-scrollbars";
import "./TripInfo.scss";
import TripPhotos from "../../components/trip-photos/TripPhotos";

interface TripInfoProps {
  photosUrls: string[];
}

export default function TripInfo(props: PropsWithChildren<TripInfoProps>) {
  return (
    <Grid id="wrapper" container justify="space-between">
      <Grid id="map-wrapper" container item direction="column" xs>
        <Box boxShadow={1} height="100%">
          <img alt="" id="map-image" src="/image-placeholder.png" />
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

const renderPhotos = (photosUrls: string[]) => (
  <Grid item>
    <TripPhotos photosUrls={photosUrls} />
  </Grid>
);
