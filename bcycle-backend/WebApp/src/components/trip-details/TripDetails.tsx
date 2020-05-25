import {Grid, Paper, Typography} from '@material-ui/core';
import React from 'react';
import './TripDetails.scss';
import PersonHeader from '../person-header/PersonHeader';
import TripMeasure from '../trip-measure/TripMeasure';
import {Person} from '../../core/model';

interface TripHeaderProps {
  tripName: string;
  subject: Person;
  measures: {
    time: number; // seconds
    distance: number; // km
    pace: number; // sec/km
    avgSpeed: number; // km/h
    started: string;
    finished: string;
  };
}

export default function TripDetails(props: TripHeaderProps) {
  const formatDoubleDigit = (digit: number) => (digit < 10 ? '0' + digit : digit.toString());
  const parseMinutes = (seconds: number) => {
    const minutes = Math.floor(seconds / 60);
    const secondsRest = seconds - minutes * 60;
    return `${formatDoubleDigit(minutes)}:${formatDoubleDigit(secondsRest)}`;
  };
  const parseHours = (seconds: number) => {
    const hours = Math.floor(seconds / 3600);
    const minutes = Math.floor((seconds - hours * 3600) / 60);
    const secondsRest = seconds - minutes * 60 - hours * 60;
    return `${formatDoubleDigit(hours)}:${formatDoubleDigit(minutes)}:${formatDoubleDigit(secondsRest)}`;
  };

  const parseDateString = (dateString: string) => {
    const date = new Date(dateString);
    return `${date.toLocaleTimeString()}\n${date.toLocaleDateString('en-gb')}`;
  };

  const measures = props.measures;

  return (
    <Paper className="section">
      <Grid container direction="column" spacing={3}>
        <Grid item>
          <Typography variant="h4">{props.tripName}</Typography>
        </Grid>
        <Grid item xs={4}>
          <PersonHeader fullName={props.subject.fullName} avatarUrl={props.subject.avatarUrl} />
        </Grid>
        <Grid id="trip-measures" container item justify="space-between">
          <Grid className="measure-column" item container xs={6} spacing={2}>
            <TripMeasure name={'Distance'} value={`${props.measures.distance} km`} />
            <TripMeasure name={'Pace'} value={`${parseMinutes(props.measures.pace)}`} />
            <TripMeasure name={'Started'} value={parseDateString(measures.started)} />
          </Grid>
          <Grid className="measure-column" item container xs={6} spacing={2}>
            <TripMeasure name={'Time'} value={parseHours(props.measures.time)} />
            <TripMeasure name={'Avg. speed '} value={`${props.measures.avgSpeed} km/h`} />
            <TripMeasure name={'Finished'} value={parseDateString(measures.finished)} />
          </Grid>
        </Grid>
      </Grid>
    </Paper>
  );
}
