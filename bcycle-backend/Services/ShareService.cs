using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bcycle_backend.Models.Entities;
using bcycle_backend.Models.Responses.Views;
using Firebase.Storage;
using static bcycle_backend.Models.Entities.ParticipantStatus;

namespace bcycle_backend.Services
{
    public class ShareService
    {
        private readonly UserService _userService;
        private readonly TripService _tripService;
        private readonly FirebaseStorage _firebaseStorage;

        public ShareService(UserService userService, TripService tripService, FirebaseStorage firebaseStorage)
        {
            _userService = userService;
            _tripService = tripService;
            _firebaseStorage = firebaseStorage;
        }

        public async Task<PrivateTripShareView> GetPrivateTripAsync(Guid sharingId)
        {
            var trip = await _tripService.FindBySharingId(sharingId);
            if (trip == null || trip.GroupTripId != null) return null;
            var photos = trip.Photos.Select(p => p.PhotoUrl);
            return new PrivateTripShareView
            {
                Subject = await FindPerson(trip.UserId),
                Route = trip.Route,
                PhotosUrls = photos,
                Measures = new Measures(trip)
            };
        }

        public async Task<GroupTripShareView> GetGroupTripAsync(Guid childSharingId)
        {
            var privateTrip = await _tripService.FindBySharingId(childSharingId);
            if (privateTrip?.GroupTrip == null) return null;
            var parentTrip = privateTrip.GroupTrip;
            var participants = FindTripParticipants(parentTrip);

            participants.Add(await FindPerson(parentTrip.HostId));
            return new GroupTripShareView
            {
                TripName = parentTrip.Name,
                Subject = await FindPerson(privateTrip.UserId),
                Measures = new Measures(privateTrip),
                Participants = participants,
                PhotosUrls = parentTrip.Trips.SelectMany(t => t.Photos).Select(p => p.PhotoUrl),
                Route = privateTrip.Route,
            };
        }

        private List<Person> FindTripParticipants(GroupTrip groupTrip) =>
            groupTrip.Participants
                .Where(p => p.Status == Accepted)
                .Select(async p => await FindPerson(p.UserId))
                .Select(t => t.Result)
                .ToList();

        private async Task<Person> FindPerson(string userId)
        {
            var userInfo = await _userService.GetUserInfoAsync(userId);
            return userInfo == null
                ? new Person {FullName = "Unknown user"}
                : new Person {FullName = userInfo.DisplayName, AvatarUrl = await GetDownloadUrl(userInfo.PhotoUrl)};
        }

        private Task<string> GetDownloadUrl(string firebaseStorageUrl) =>
            _firebaseStorage.Child(firebaseStorageUrl.Split('/').Last()).GetDownloadUrlAsync();
    }
}
