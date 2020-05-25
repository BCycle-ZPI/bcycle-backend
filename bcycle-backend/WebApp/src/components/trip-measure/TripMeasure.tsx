import {Grid, Typography} from '@material-ui/core';
import React from 'react';
import './TripMeasure.scss';

interface TripMeasureProps {
  name: string;
  value: string;
}

export default function TripMeasure(props: TripMeasureProps) {
  return (
    <Grid item container alignItems="flex-start">
      <Grid item xs>
        <Typography className="measure-name">{props.name}</Typography>
      </Grid>
      <Grid item xs>
        <Typography className="measure-value">{props.value}</Typography>
      </Grid>
    </Grid>
  );
}
