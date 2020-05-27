import { Grid, LinearProgress } from '@material-ui/core';
import React from 'react';

const Loading = () => (
  <Grid id="loading-wrapper" container>
    <Grid item xs>
      <LinearProgress variant="query" color="secondary" />
    </Grid>
  </Grid>
);

export default Loading;
