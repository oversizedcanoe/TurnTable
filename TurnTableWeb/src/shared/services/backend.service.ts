import { HttpClient, HttpErrorResponse, HttpHeaders, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class BackendService {
  public static readonly BackendUrl: string = 'https://192.168.2.34:7282';
  private readonly apiUrl: string = BackendService.BackendUrl + '/api/'

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
    const result$ = this.httpClient.post<T>(this.apiUrl + url, body, { observe: 'response' });

    try {
      const result = await lastValueFrom(result$);
      return result.body as T;
    }
    catch (error) {
        this.handleError(error);
    }

    return null;
  }

  handleError(error: any){
    console.error(error);

    if(error instanceof HttpErrorResponse){
      alert(`Sorry, an error occurred (HTTP ${error.status})`);
    } else {
      alert('Unknown error');
    }
  }
}

