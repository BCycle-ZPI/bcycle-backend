import { Grid } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import { PrivateTrip } from "../../core/model";
import { useParams } from "react-router";
import { getPrivateTrip } from "../../core/trips-client";
import NotFound from "../../components/not-found/NotFound";
import TripDetails from "../../components/trip-details/TripDetails";
import Loading from "../../components/loading/Loading";
import TripInfo from "../trip-info/TripInfo";

interface PrivateTripInfoState {
  trip: PrivateTrip | undefined;
}

export default function PrivateTripInfo() {
  const { tripId } = useParams();
  const [state, setState] = useState<PrivateTripInfoState>();

  useEffect(() => {
    setState({ trip: getPrivateTrip(+tripId) });
  }, []);

  if (!state) return <Loading />;
  const tripInfo = state.trip;
  if (!tripInfo) return <NotFound />;

  return (
    <TripInfo photosUrls={tripInfo.photosUrls}>
      <Grid item>
        <TripDetails
          tripName={`Trip ${parseTripDate(tripInfo.measures.started)}`}
          subject={tripInfo.subject}
          measures={tripInfo.measures}
        />
      </Grid>
    </TripInfo>
  );
}

const parseTripDate = (date: string) => new Date(date).toLocaleDateString('en-gb');
