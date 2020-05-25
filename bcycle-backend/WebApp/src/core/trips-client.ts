import { GroupTrip, PrivateTrip } from './model';
import { groupTrip, privateTrip } from './dummy-data';

// TODO: connect to backend

export function getParentGroupTrip(childTripId: number): GroupTrip | undefined {
  if (childTripId === 1) return groupTrip;
  else return undefined;
}

export function getPrivateTrip(tripId: number): PrivateTrip | undefined {
  if (tripId === 1) return privateTrip;
  else return undefined;
}
