import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Subscription} from 'rxjs';
import {ConfigModel} from '../models/config.model';
import {ConfigService} from '../services/config.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {

    private apiQuery: Subscription;
    config: ConfigModel = {rssLink: null, refreshFrequency: null};

    settingsForm: FormGroup = new FormGroup({
        link: new FormControl('', [
            Validators.required,
            Validators.pattern('(http|ftp|https)://[\\w-]+(\\.[\\w-]+)+([\\w.,@?^=%&amp;:/~+#-]*[\\w@?^=%&amp;/~+#-])?')
        ]),
        refreshRate: new FormControl('', [
            Validators.required,
            Validators.pattern('[0-9]*\.?[0-9]+')
        ])
    });

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl, private configService: ConfigService) {
    }

    async ngOnInit() {
        this.config = await this.configService.getSettings();
        console.log(this.config);
    }

    saveConfig() {
        console.log(this.config);
        this.config = {rssLink: this.settingsForm.value.link, refreshFrequency: this.settingsForm.value.refreshRate};
        this.apiQuery = this.http.put(this.baseUrl + 'api/settings', {
            RssLink: this.settingsForm.value.link,
            RefreshFrequency: this.settingsForm.value.refreshRate
        }).subscribe(res => {
            console.log(res);
            this.configService.saveSettings(this.config);
        }, error => {
            console.log(error);
        });
    }
}
