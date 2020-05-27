export interface PrivateTrip {
    subject: Person;
    measures: Measures
    photosUrls: string[];
    route: RoutePoint[];
}

export interface GroupTrip {
    tripName: string;
    subject: Person;
    measures: Measures
    participants: Person[];
    photosUrls: string[];
    route: RoutePoint[];
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

export interface RoutePoint {
    timeReached: string;
    latitude: number;
    longitude: number;
}
