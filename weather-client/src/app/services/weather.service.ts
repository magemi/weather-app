import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Weather } from '../interfaces/weather';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class WeatherService {

  constructor(private http: HttpClient) { }

  getWeatherData(): Observable<Weather[]> {
    return this.http.get<Weather[]>('https://localhost:7060/getWeatherData', {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    });
  }

  setTemperatureUnit(temperatureUnit: string): Observable<string> {
    return this.http.post<string>('https://localhost:7060/setTemperatureUnit', null, {
      params: { temperatureUnit: temperatureUnit },
    });
  }
}
