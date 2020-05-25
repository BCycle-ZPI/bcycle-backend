import {Grid, Paper, Typography} from '@material-ui/core';
import React from 'react';
import './TripParticipants.scss';
import PersonHeader from '../person-header/PersonHeader';

interface TripParticipantsProps {
  participants: { fullName: string; avatarUrl: string }[];
}

export default function TripParticipants(props: TripParticipantsProps) {
  return (
    <Paper className="section">
      <Grid container direction="column">
        <Grid item>
          <Typography className="section-header">Participants</Typography>
        </Grid>
        <Grid item container direction="row" spacing={2}>
          {props.participants.map((p, i) => (
            <Grid key={i} item xs={4}>
              <PersonHeader fullName={p.fullName} avatarUrl={p.avatarUrl} />
            </Grid>
          ))}
        </Grid>
      </Grid>
    </Paper>
  );
}
