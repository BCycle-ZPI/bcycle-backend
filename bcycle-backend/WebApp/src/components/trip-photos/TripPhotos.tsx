import {Grid, Paper, Typography} from '@material-ui/core';
import React from 'react';
import './TripPhotos.scss';

interface TripPhotosProps {
  photosUrls: string[];
}

export default function TripPhotos(props: TripPhotosProps) {
  const renderPhotos = () => (
    <Grid container spacing={1}>
      {props.photosUrls.map((url, i) => (
        <Grid key={i} item xs={4}>
          <img alt="" className="trip-photo" key={url} src={url} />
        </Grid>
      ))}
    </Grid>
  );
  const renderNoPhotosText = () => <Typography id="no-photos">No photos has been taken</Typography>;

  return (
    <Paper id="trip-photos" className="section section-last">
      <Grid container direction="column">
        <Grid item>
          <Typography className="section-header">Photos</Typography>
        </Grid>
        <Grid item>{props.photosUrls.length === 0 ? renderNoPhotosText() : renderPhotos()}</Grid>
      </Grid>
    </Paper>
  );
}
