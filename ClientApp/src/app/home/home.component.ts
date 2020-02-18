import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {of, Subscription, timer} from 'rxjs';
import {ItemModel} from '../models/item.model';
import {ConfigService} from '../services/config.service';
import {ConfigModel} from '../models/config.model';
import {catchError, concatMap, expand, map} from 'rxjs/operators';

@Component({
    selector: 'app-home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, OnDestroy {

    private apiQuerySubscription: Subscription;
    private config: ConfigModel;
    articles: ItemModel[] = [];

    constructor(private http: HttpClient, @Inject('BASE_URL') private baseUrl, private configService: ConfigService) {
    }

    async ngOnInit() {
        this.config = await this.configService.getSettings();
        this.getArticles();
    }

    ngOnDestroy() {
        this.articles = [];
        this.apiQuerySubscription.unsubscribe();
    }

    private async getArticles() {
        const apiQuery = this.http.get(this.baseUrl + 'api/rss')
            .pipe(
                map((res: ItemModel[]) => {
                    console.log('Refreshing articles');
                    res.forEach((el, index, array) => {
                        const date = new Date(el.pubDate);
                        array[index].pubDate = date.getDay() + '-' + date.getMonth() + '-' + date.getFullYear() + ' at ' + date.getHours() + ':' + date.getMinutes();
                    });
                    this.articles = res;
                }),
                catchError(err => of(`I caught: ${err}`))
            );

        this.apiQuerySubscription = apiQuery.pipe(
            expand(idk => timer(this.config.refreshFrequency * 3600000).pipe(concatMap(_ => apiQuery)))
        ).subscribe();
    }

    onTitleClick(index: number) {
        window.open(this.articles[index].link, '_blank');
    }
}
