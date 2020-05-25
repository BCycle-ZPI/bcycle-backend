import { GroupTrip, PrivateTrip } from "./model";

export const groupTrip: GroupTrip = {
  tripName: 'Some awesome trip',
  subject: { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
  measures: {
    time: 36 * 60,
    distance: 24.23,
    pace: 150,
    avgSpeed: 21.2,
    started: '02/12/2019 12:32',
    finished: '02/12/2019 12:59',
  },
  participants: [
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
    { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
  ],
  photosUrls: [
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
  ],
  path: [],
};

export const privateTrip: PrivateTrip = {
  subject: { fullName: 'Jan Kowalski', avatarUrl: 'avatar-placeholder.png' },
  measures: {
    time: 36 * 60,
    distance: 24.23,
    pace: 150,
    avgSpeed: 21.2,
    started: '02/12/2019 12:32',
    finished: '02/12/2019 12:59',
  },
  photosUrls: [
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
    '/image-placeholder.png',
  ],
  path: [],
};

