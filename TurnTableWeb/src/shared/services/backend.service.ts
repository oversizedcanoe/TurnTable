import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  baseUrl: string = 'https://localhost:7084/api/'
  constructor(public httpClient: HttpClient) {
  }

//   async get<T>(url: string): Promise<T> {
//     const result$ = this.httpClient.get(this.baseUrl + url, { observe: 'response'});
//     const result = await lastValueFrom(result$);

//     if (result.ok) {
//       return result.body as T;
//     }
//     else {
//       alert(`Error ${result.status}: ${result.statusText}`)
//       console.error(result);
//       return new ApiError(result.statusText, result.status)
//     }
//   }

  async post<T>(url: string, body: {} = {}): Promise<T | null> {
    console.log(body, typeof body);
    const result$ = this.httpClient.post<T>(this.baseUrl + url, body, { observe: 'response'});
    const result = await lastValueFrom(result$);
    
    if (result.ok) {
      return result.body as T;
    }
    else {
      alert(`Error ${result.status}: ${result.statusText}`)
      console.error(result);
      return null ;//new ApiError(result.statusText, result.status)
    }
  }
}

