import { Component, Input } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bus-results',
  templateUrl: './bus-results.component.html',
  styleUrls: ['./bus-results.component.css']
})
export class BusResultsComponent {
  @Input() buses: any[] = [];
  selectedBusId: number | null = null;
  selectedBusSeats: any[] = [];

  constructor(private http: HttpClient) {}

  viewSeats(busId: number) {
    if (this.selectedBusId === busId) {
      this.selectedBusId = null;
      this.selectedBusSeats = [];
      return;
    }

    this.selectedBusId = busId;

   this.http.get<any[]>(`https://localhost:57263/api/seats/bus/${busId}`).subscribe({
      next: (res) => this.selectedBusSeats = res,
      error: (err) => console.error('Error fetching seats', err)
    });
  }

  bookSeat(busId: number, seat: any) {
    if (seat.isBooked) {
      alert('Seat already booked!');
      return;
    }

    seat.isBooked = true;
    alert(`Seat ${seat.seatNo} booked successfully!`);
  }

  totalSeats(): number {
    if (!this.buses || this.buses.length === 0) return 0;
    return this.buses.reduce((sum, bus) => {
      const seatsLeft = bus.totalSeats != null && bus.bookedSeats != null
        ? bus.totalSeats - bus.bookedSeats
        : 0;
      return sum + seatsLeft;
    }, 0);
  }
}
