const login={template:`
<div id="container-login">
        <h3>Login</h3>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <label >E-mail:</label>
                    <input type="text" class="form-control" v-model="email"/>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="row">
                <div class="col-md-12">
                    <label >Senha:</label>
                    <input type="password" class="form-control" v-model="senha"/>
                </div>
            </div>
        </div>
        <router-link  class="btn btn-link" id="btn-esqueceu" to="/esqueceu">Esqueceu a senha?</router-link>
        <div class="form-group">
            <div class="row">
                <div class="col-md-6">
                    <button type="button" @click="loginClick()" class="btn btn-light btn-outline-primary" id="ovBTN_login">Entrar</button>
                </div>
            </div>
            
        <label>Não possui conta?</label>
        
        <router-link  class="btn btn-link" id="btn-novo" to="/usuario">Crie sua conta</router-link>
    </div>  
    </div>             
`,
data(){
    return{
        email:"",
        senha:"",
    }
},
methods:{
    loginClick(){
        axios.get(variaveis.API_URL+"usuario",{
            email:this.email,
            senha:this.senha
        }).then((response)=>{
            alert(response.data);
        });
    }
}

}