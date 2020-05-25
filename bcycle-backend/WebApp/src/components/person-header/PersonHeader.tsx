import { Avatar, Grid, Typography } from '@material-ui/core';
import React from 'react';
import './PersonHeader.scss';

interface TripParticipantProps {
  avatarUrl: string;
  fullName: string;
}

export default function PersonHeader(props: TripParticipantProps) {
  return (
    <Grid item container spacing={2} alignItems="center">
      <Grid item xs={3}>
        <Avatar src={props.avatarUrl} />
      </Grid>
      <Grid item xs={9}>
        <Typography className="person-name">{props.fullName}</Typography>
      </Grid>
    </Grid>
  );
}
