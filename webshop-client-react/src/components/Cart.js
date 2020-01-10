import React from 'react'; 
import CartProduct from './CartProduct';   
import { WebshopEndpoint } from "../constants"; 
//import { BrandsEndpoint } from "../constants";   
  
class Cart extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      items: [],
      loading: true,
      isEmpty: false   
    };   
   this.removeItemFromCartItemsState = this.removeItemFromCartItemsState.bind(this);
   this.updateTotalCartQuantity = this.updateTotalCartQuantity.bind(this); 
   this.updateCartItemQuantity = this.updateCartItemQuantity.bind(this); 
   this.itemDelete = this.itemDelete.bind(this); 
  }   
  // Bliver kaldt når dette component bliver indsat ind i dom træet for første gang
  componentDidMount() {
    const url = WebshopEndpoint + "/ShoppingCartController/GetAllCartItems";  
    
    fetch(url)
      .then(response => response.json()) 
      .then(json => {
          this.setState({ items: json, isEmpty: false}, function () {
           
          
        }); 
        }) 
      .catch(() => {
        console.log("No items in cart");  
        this.setState({ isEmpty: true});
      });
      this.setState({ loading: false});
  
}


  // filtrere this.state.items og fjerner en specificeret item 
  removeItemFromCartItemsState(detailID) {

    this.setState(prevState => {
      const updatedItems = prevState.items.filter(item => item.cartDetailID !== detailID)
  
      return { 
        items: updatedItems,
        isEmpty: updatedItems.length <= 0 ? true : false 
      } 
    })
    this.updateTotalCartQuantity();
  } 
  
// opdatere cart quantity sum
updateTotalCartQuantity () { 
    this.props.updateTotalItemQuan();  
}
 
 // kalder bestemt endpoint der fjerne et specificeret item fra cart 
  itemDelete(cartDetailID) {
 
    const url = WebshopEndpoint + "/ShoppingCartController/RemoveItemFromCart/" +
      cartDetailID;
    fetch(url, {
      method: "delete" 
    })
      .then(response => response.json())
      .then(() => {
        this.removeItemFromCartItemsState(cartDetailID);
      })
      .catch(() => {
        console.log("An error occured");
      });; 
}  
   // kalder bestemt endpoint der opdatere et specificeret cart-items quantity 
  updateCartItemQuantity(amount, cartDetailID) {
    const url = WebshopEndpoint + "/ShoppingCartController/UpdateItemQuan/" +
    cartDetailID; 

    fetch(url, { 
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        dataType: "application/json"
      },
      body: JSON.stringify(amount)
    })
      .then(response => response.json())
      .then((json) => {
        switch (json.id) {
          case 603:
            this.setState(prevState => {
              const updatedItems = prevState.items.map(prod => {
                if (prod.cartDetailID === cartDetailID) {
                  prod.quantity = json.result
                }
                return prod
              })
              return {
                items: updatedItems
              }
            }) 
            break;
          case 602:
            this.removeItemFromCartItemsState(cartDetailID);
            break;
          default:
            // Logger besked så vi ved hvilken fejl der forekom 
            console.log(json.message)
        }
      }).then(() => this.updateTotalCartQuantity())
      .catch(() => {
        console.log("An error occured");
      });;
  }
  
  // kaldes hver gang this.state ændres
  render() {  

    let items = this.state.isEmpty ? ( 
    <p>
      <em>The cart is empty...</em> 
    </p>
    ) : ( 
   
      this.state.items.map(element => (
      
          <CartProduct  
          key = {element.cartDetailID} 
          cartDetailID={element.cartDetailID}  
          quantity={element.quantity} 
          product_id={element.product_id}
          product_img={element.product_img} 
          product_brand={element.product_brand}
          product_name={element.product_name}
          product_price={element.product_price}
          delItem={this.itemDelete} 
          updateItemQuan = {this.updateCartItemQuantity} 
          /> 
        
        )) 
    );   
    let contents = this.state.loading ? ( 
        <p>
          <em>Loading...</em> 
        </p>
      ) : ( 
      
        <div id="Cart">
          <div className="title">Shopping Cart</div>
       
            {items}
           
        </div>
     
      ); 
    return ( 
     
      <div>
        {contents}
      </div>

    ); 
  }
}  
export default Cart 