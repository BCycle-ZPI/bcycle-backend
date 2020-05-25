import { CircularProgress, Grid, LinearProgress, Typography } from "@material-ui/core";
import React from "react";
import './Loading.scss';

const Loading = () => (
  <Grid id='loading-wrapper' container>
    <Grid item xs>
      <LinearProgress variant='query' color='secondary'/>
    </Grid>
  </Grid>
);

export default Loading;
