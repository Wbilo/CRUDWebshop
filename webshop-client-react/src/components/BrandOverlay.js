import * as React from "react";
import "../css/BrandOverlayStyle.css"; 
import ShoeRackImg from '../img/shoe-rack.jpg';              

// Benytter function istedet for class da dette component ikke arbejder
// med en state og er mere simpelt 
function BrandOverlay(props) {
    var img = props.brand_banner ? props.brand_banner : ShoeRackImg;
    /* Hvis der er info text med til det nuværende brand
      så tilføjer vi infoText class ellers bare en tom string*/ 
    var infoTextClass = props.brand_info ? "infoText" : ""; 
    return (
        <div className="container">
            <img alt="" className="brandImg" src={img}></img> 
            {/* her putter vi den prop "brand_name" der bliver parsed til komponentet
            ind i div'en*/}  
            <div className="centered">{props.brand_name}</div>
            <div className={infoTextClass}>{props.brand_info}</div>
        </div>   
    )   
} 

export default BrandOverlay;   
