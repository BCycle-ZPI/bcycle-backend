import { Grid } from "@material-ui/core";
import React, { useEffect, useState } from "react";
import TripDetails from "../../components/trip-details/TripDetails";
import TripParticipants from "../../components/trip-participants/TripParticipants";
import { GroupTrip } from "../../core/model";
import { getParentGroupTrip } from "../../core/trips-client";
import { useParams } from "react-router";
import NotFound from "../../components/not-found/NotFound";
import Loading from "../../components/loading/Loading";
import TripInfo from "../trip-info/TripInfo";

interface GroupTripInfoState {
  parentGroupTrip: GroupTrip | undefined;
}

export default function GroupTripInfo() {
  const { childTripId } = useParams();
  const [state, setState] = useState<GroupTripInfoState>();
  useEffect(() => {
    setState({ parentGroupTrip: getParentGroupTrip(+childTripId) });
  }, []);

  if (!state) return <Loading />;
  const tripInfo = state.parentGroupTrip;
  if (!tripInfo) return <NotFound />;

  return (
    <TripInfo photosUrls={tripInfo.photosUrls}>
      <Grid item>
        <TripDetails tripName={tripInfo.tripName} subject={tripInfo.subject} measures={tripInfo.measures} />
      </Grid>
      <Grid item>
        <TripParticipants participants={tripInfo.participants} />
      </Grid>
    </TripInfo>
  );
}
