import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ConfigModel} from '../models/config.model';

@Injectable({
    providedIn: 'root'
})
export class ConfigService {

    private config: ConfigModel = null;

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl: string) {
    }

    async getSettings(): Promise<ConfigModel> {
        if (this.config) {
            return this.config;
        }
        return await this.http.get<ConfigModel>(this.baseUrl + 'api/settings').toPromise();
    }

    saveSettings(config: ConfigModel) {
        this.config = config;
    }
}
