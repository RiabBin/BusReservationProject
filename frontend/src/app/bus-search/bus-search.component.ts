import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-bus-search',
  templateUrl: './bus-search.component.html',
  styleUrls: ['./bus-search.component.css']
})
export class BusSearchComponent {
  from = 'Sylhet';
  to = 'Dhaka';
  date = new Date().toISOString().substring(0,10);
  buses: any[] = [];

  constructor(private http: HttpClient) {}


  swap() {
    const temp = this.from;
    this.from = this.to;
    this.to = temp;
  }

  search() {
  const api = `https://localhost:57263/api/bus/searchBus?from=${this.from}&to=${this.to}&date=${this.date}`;

  console.log('API Call:', api); 
  this.http.get<any[]>(api).subscribe(
    res => {
      this.buses = res;
      console.log('Response:', res);
    },
    err => console.error('Error:', err)
  );
}

}
