import { Component, computed, input } from '@angular/core';
import { Weather } from '../interfaces/weather';

@Component({
  selector: 'app-weather-card',
  imports: [],
  templateUrl: './weather-card.component.html',
  styleUrl: './weather-card.component.scss'
})
export class WeatherCardComponent {
  useImperialMeasurementSystem = input<boolean>();
  showSunset = input<boolean>();
  weather = input<Weather>();

  temperature = computed(() => Math.round(this.weather()?.temperature!));

  sunrise = computed(() => {
    const locale = this.useImperialMeasurementSystem() ? "en-US" : new Intl.Locale("en-US", { hourCycle: "h24" });
    return new Date(this.weather()?.sunrise! * 1000).toLocaleTimeString(locale, { timeStyle: 'short' });
  });

  sunset = computed(() => {
    const locale = this.useImperialMeasurementSystem() ? "en-US" : new Intl.Locale("en-US", { hourCycle: "h24" });
    return new Date(this.weather()?.sunset! * 1000).toLocaleTimeString(locale, { timeStyle: 'short' });
  });
}
