import React from 'react';
import Product from "./Product";
import BrandOverlay from './BrandOverlay';
import { WebshopEndpoint } from "../constants";
import { BrandsEndpoint } from "../constants";

class ProductList extends React.Component {
  constructor() {
    super();

    this.state = {
      products: [],
      brand: {},
      frontPageBannerText: "Welcome to The Shoe Shop!",
      frontPageEndPoint: "/" 

    };
    this.getBrandInfo = this.getBrandInfo.bind(this);

  }
    // Bliver kaldt når dette component bliver indsat ind i dom træet for første gang  
  componentDidMount() {

    this.getProductData();
  } 
  componentDidUpdate(prevProps) {

    // Får fat i search frase i url'en  
    let currentSearch = this.props.location.search;
    let oldSearch = prevProps.location.search;


    // kun refresh hvis søgning ændret  
    if (currentSearch !== oldSearch) {
      this.getProductData();
    }


  }
   // Kalder endpoint som retunere info om brands 
  getBrandInfo(url) {
    fetch(url, {

    })
      .then(response => {
        // transformere responsen fra API'et om til json-data
        return response.json();
      })
      .then(data => {
        this.setState({
          brand: data
        });
      }).catch(() => {
        console.log("An error occured");
      });
  }

   // Kalder bl.a. endpoint som retunere info om produkter  
  getProductData() {

    var url = "";
    // Får fat i search frase i url'en og fjerner første char som er et question mark 
    var brand = this.props.location.search.substr(1);
    // Hvis forsider se hent alle produkters info
    if (this.props.location.pathname === this.state.frontPageEndPoint) {

      url = WebshopEndpoint + "/Webshop/GetAllProductInfo";

    }
    // Ellers hent products fra den søgte brand
    else {
      url = WebshopEndpoint + "/Webshop/GetProductsByBrand/" + brand;
      // fetcher det andet api her (BrandsAPI). 
      this.getBrandInfo(BrandsEndpoint + "/GetBrandInfo/" + brand);

    }

    // Sender et aynkront request til API
    fetch(url, {

    })
      .then(response => {
        return response.json();
      })
      .then(data => {
        this.setState({
          products: data
        });
      }).catch(() => {
        console.log("An error occured");
      });


  }







  // returner et object med Product komponenter for hver eneste entry i det array der 
  // bliver parsed som parameterværdi
  renderProducts(prods, currentBrand) {
    var searchedBrand = this.props.location.search.substr(1);
    var frontPage = this.props.location.pathname === this.state.frontPageEndPoint ? true : false;
    // Sætter første bogstav til stort bogstav
    var brandBannerText = searchedBrand.charAt(0).toUpperCase() + searchedBrand.substring(1);
    var bannerText = frontPage ? this.state.frontPageBannerText : brandBannerText;

    return (
      <div className="row">
        <BrandOverlay
          brand_name={bannerText}
          brand_banner={currentBrand.brand_banner}
          brand_info={currentBrand.brand_info}
        />


        {prods.map(prod => (
          <Product
            key={prod.product_id}
            name={prod.product_name}
            brand={prod.product_brand}
            img={prod.product_img}
            price={prod.product_price}
            inStock={prod.product_inStock}
            handleClick={() => this.props.addToCart(prod.product_id)}
          />

        ))}
      </div>
    );
  }
  // bliver kaldt når "state" ændres  
  render() {

    let contents = (
      this.renderProducts(this.state.products, this.state.brand)
    );

    return (
      <div>{contents}</div>
    );
  }
}

export default ProductList;



