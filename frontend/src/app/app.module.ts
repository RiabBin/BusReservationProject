import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { BusSearchComponent } from './bus-search/bus-search.component';
import { BusResultsComponent } from './bus-results/bus-results.component';

@NgModule({
  declarations: [
    AppComponent,
    BusSearchComponent,
    BusResultsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
