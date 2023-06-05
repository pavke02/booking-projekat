using Microsoft.Extensions.DependencyInjection;
using SIMS_Booking.Model;
using SIMS_Booking.Model.Relations;
using SIMS_Booking.Repository;
using SIMS_Booking.Service;
using SIMS_Booking.Service.RelationsService;
using SIMS_Booking.UI.Utility;
using SIMS_Booking.UI.ViewModel.Startup;
using SIMS_Booking.Utility.Stores;
using System.Windows;
using System.Windows.Controls;

namespace SIMS_Booking
{
    public partial class App : Application
    {
        private static ServiceProvider _services;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.AddRepositories();
            services.AddServices();
            services.AddSingleton<NavigationStore>();
            services.AddSingleton<ModalNavigationStore>();
            services.AddViewModels();

            _services = services.BuildServiceProvider();

            _services.GetRequiredService<NavigationStore>().CurrentViewModel = _services.GetRequiredService<SignInViewModel>();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_services.GetRequiredService<NavigationStore>(), _services.GetRequiredService<ModalNavigationStore>())
            };
            MainWindow.Show();
            base.OnStartup(e);
        }
        public static T GetViewModel<T>() where T : ViewModelBase
        {
            return _services.GetRequiredService<T>();
        }
    }
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<ICRUDRepository<User>, CsvCrudRepository<User>>();
        
            services.AddSingleton<ICRUDRepository<Accommodation>, CsvCrudRepository<Accommodation>>();
            services.AddSingleton<ICRUDRepository<Reservation>, CsvCrudRepository<Reservation>>();
            services.AddSingleton<ICRUDRepository<Postponement>, CsvCrudRepository<Postponement>>();
            services.AddSingleton<ICRUDRepository<OwnerReview>, CsvCrudRepository<OwnerReview>>();
            services.AddSingleton<ICRUDRepository<RenovationAppointment>, CsvCrudRepository<RenovationAppointment>>();
            services.AddSingleton<ICRUDRepository<GuestReview>, CsvCrudRepository<GuestReview>>();
            services.AddSingleton<ICRUDRepository<UsersAccommodation>, CsvCrudRepository<UsersAccommodation>>();
            services.AddSingleton<ICRUDRepository<ReservedAccommodation>, CsvCrudRepository<ReservedAccommodation>>();
            services.AddSingleton<CityCountryCsvRepository>();

            services.AddSingleton<ICRUDRepository<ConfirmTour>, CsvCrudRepository<ConfirmTour>>();
            services.AddSingleton<ICRUDRepository<DriverLanguages>, CsvCrudRepository<DriverLanguages>>();
            services.AddSingleton<ICRUDRepository<DriverLocations>, CsvCrudRepository<DriverLocations>>();
            services.AddSingleton<ICRUDRepository<FinishedRide>, CsvCrudRepository<FinishedRide>>();
            services.AddSingleton<ICRUDRepository<GuideReview>, CsvCrudRepository<GuideReview>>();
            services.AddSingleton<ICRUDRepository<Rides>, CsvCrudRepository<Rides>>();
            services.AddSingleton<ICRUDRepository<TourPoint>, CsvCrudRepository<TourPoint>>();
            services.AddSingleton<ICRUDRepository<TourReview>, CsvCrudRepository<TourReview>>();
            services.AddSingleton<ICRUDRepository<Tour>, CsvCrudRepository<Tour>>();
            services.AddSingleton<ICRUDRepository<VehicleReservation>, CsvCrudRepository<VehicleReservation>>();
            services.AddSingleton<ICRUDRepository<Vehicle>, CsvCrudRepository<Vehicle>>();
            services.AddSingleton<ICRUDRepository<Voucher>, CsvCrudRepository<Voucher>>();
            services.AddSingleton<ICRUDRepository<TourReservation>, CsvCrudRepository<TourReservation>>();
            services.AddSingleton<ICRUDRepository<ReservationOfVehicle>, CsvCrudRepository<ReservationOfVehicle>>();
            services.AddSingleton<ICRUDRepository<TourRequest>,CsvCrudRepository <TourRequest>>();
            services.AddSingleton<TextBox>();
            services.AddSingleton<ICRUDRepository<GroupRide>, CsvCrudRepository<GroupRide>>();
            services.AddSingleton<ICRUDRepository<TourRequest>, CsvCrudRepository<TourRequest>>();
            services.AddSingleton<ICRUDRepository<Comment>, CsvCrudRepository<Comment>>();
            services.AddSingleton<ICRUDRepository<Forum>, CsvCrudRepository<Forum>>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<UserService>();

            services.AddSingleton<AccommodationService>();
            services.AddSingleton<ReservationService>();
            services.AddSingleton<PostponementService>();
            services.AddSingleton<OwnerReviewService>();
            services.AddSingleton<RenovationAppointmentService>();
            services.AddSingleton<GuestReviewService>();
            services.AddSingleton<ReservedAccommodationService>();
            services.AddSingleton<UsersAccommodationService>();
            services.AddSingleton<ForumService>();
            services.AddSingleton<CommentService>();

            services.AddSingleton<DriverLanguagesService>();
            services.AddSingleton<DriverLocationsService>();
            services.AddSingleton<ReservedTourService>();
            services.AddSingleton<ConfirmTourService>();
            services.AddSingleton<FinishedRidesService>();
            services.AddSingleton<GuideReviewService>();
            services.AddSingleton<RidesService>();
            services.AddSingleton<TourPointService>();
            services.AddSingleton<TourReviewService>();
            services.AddSingleton<TourService>();
            services.AddSingleton<VehicleReservationService>();
            services.AddSingleton<VehicleService>();
            services.AddSingleton<VoucherService>();
            services.AddSingleton<GroupRideService>();
            services.AddSingleton<TourRequestService>();

            return services;
        }

        public static IServiceCollection AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<SignInViewModel>();

            return services;
        }
    }
}
