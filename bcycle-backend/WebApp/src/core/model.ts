export interface PrivateTrip {
    subject: Person;
    measures: Measures
    photosUrls: string[];
    path: PathPoint[];
}

export interface GroupTrip {
    tripName: string;
    subject: Person;
    measures: Measures
    participants: Person[];
    photosUrls: string[];
    path: PathPoint[];
}

export interface Measures {
    time: number; // seconds
    distance: number; // km
    pace: number; // sec/km
    avgSpeed: number; // km/h
    started: string;
    finished: string;
}

export interface Person {
    fullName: string
    avatarUrl: string
}
export interface PathPoint {
    order: number;
    latitude: number;
    longitude: number;
}
