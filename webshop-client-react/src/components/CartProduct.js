import React from 'react'; 
import minusImg from '../img/minus.svg'; 
import plusImg from  '../img/plus.svg'; 
class CartProduct extends React.Component { 
  render() {  
    return (
    <div className="Prod"> 
      <span onClick={() => this.props.delItem(this.props.cartDetailID)} className="del-but"></span>
      <div className="ProdImg">  
    <img src={this.props.product_img}  alt=""></img> 
      </div>     
      <div className="descrip">
        <span className="name">{this.props.product_name}</span>   
        <span className="brand">{this.props.product_brand}</span>
      </div>
      <div className="quantity">
        <button className="plus-but" onClick={() => this.props.updateItemQuan(1, this.props.cartDetailID)}>
          <img src={plusImg} alt=""></img>  
        </button>
        <input type="text" name="quan" readOnly value={this.props.quantity}></input>   
        <button className="min-but" onClick={() => this.props.updateItemQuan(-1, this.props.cartDetailID)}> 
          <img src={minusImg} alt=""></img>    
        </button>
        <div className="price">{this.props.product_price} kr.</div> 
      </div>   
    </div>    
    );
  } 
}


export default CartProduct

