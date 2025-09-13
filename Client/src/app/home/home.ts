import { Component, inject } from '@angular/core';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [RouterModule],
  standalone: true,
  templateUrl: './home.html',
  styleUrl: './home.scss'
})
export class Home {
  private router = inject(Router);

    categories = [
    { id: '63ca5d4bc3a8a58f47299f97', name: 'Shoes', image: '../../../assets/images/products/adidas_shoe-1.png' },
    { id: '63ca5d6d958e43ee1cd375fe', name: 'Rackets', image: '../../../assets/images/products/babolat-racket-2.png' },
    { id: '63ca5d7d380402dce7f06ebc', name: 'Football', image: '../../../assets/images/products/adidas_football-1.png' },
    { id: '63ca5d8849bc19321b8be5f1', name: 'Kit Bags', image: '../../../assets/images/products/babolat-kitback-3.png' }
  ];

  goToCategory(cat: any){
    this.router.navigate(['/store'], {
      queryParams: {typeId: cat.id} // Pass Catagory Id
    });
  }

}
