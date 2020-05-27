import { GroupTrip, PrivateTrip } from './model';


export function getPrivateTrip(shareId: string): Promise<PrivateTrip | undefined> {
  return requestData('trip', shareId);
}

export function getParentGroupTrip(shareId: string): Promise<GroupTrip | undefined> {
  return requestData('group-trip', shareId);
}

function requestData(subPath: string, shareId: string) {
  return fetch(`${process.env.REACT_APP_API_URL}/share/${subPath}/${shareId}`).then((r) =>
    r.status === 200 ? r.json().then((j) => j.result) : undefined
  );
}
