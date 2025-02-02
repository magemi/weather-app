import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import { WeatherCardComponent } from './weather-card/weather-card.component';
import { WeatherService } from './services/weather.service';
import { Weather } from './interfaces/weather';

@Component({
  selector: 'app-root',
  imports: [FormsModule, RouterOutlet, WeatherCardComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'weather-client';
  useImperialMeasurementSystem: boolean = false;
  showSunset: boolean = false;
  weatherService: WeatherService = inject(WeatherService);
  weatherData: Weather[] = [];

  constructor() {
    this.weatherService.getWeatherData().subscribe(res => {
      this.weatherData = res;
    });
  }

  toggleMeasurementSystem(): void {
    this.weatherService.setTemperatureUnit(this.useImperialMeasurementSystem ? 'imperial' : 'metric').subscribe(res => console.log(res));
    this.weatherService.getWeatherData().subscribe(res => {
      this.weatherData = res;
    });
  }
}
