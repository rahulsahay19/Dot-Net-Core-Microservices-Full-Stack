import { CommonModule } from '@angular/common';
import { Component, computed } from '@angular/core';
import { LoadingService } from '../services/loading.service';

@Component({
  selector: 'app-loading',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './loading.html',
  styleUrls: ['./loading.scss']
})
export class Loading {
  constructor(private loadingService: LoadingService) { }
  isLoading = computed(() => this.loadingService.loading());
}
